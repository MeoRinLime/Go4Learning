// Types

export type LearningTree = {
  name: string,
  child?: LearningTree[],
  value?: string
}

export type Wrapper<T> = {
  code: 0 | 1,
  msg: string,
  data: T
}

export type CourseResponse = {
  courseId: number,
  courseName: string,
  createTime: string,
  courseState: number,
  courseCreatorId: number,
  referenceSites: {
    webSiteId: number,
    webSiteName: string,
    webSiteType: number,
    webSiteUrl: string,
    courseId: number
  }[],
  recommendedCourses: {
    webSiteId: number,
    webSiteName: string,
    webSiteType: number,
    webSiteUrl: string,
    courseId: number
  }[],
  knowledgePoints: {
    knowledgeId: number,
    knowledgePointContent: string,
    knowledgePointName: string,
    courseId: number
  }[]
}[]

export function toLearningTree(val: CourseResponse) : LearningTree[] {
  let result = new Array<LearningTree>;
  for (let item of val) {
    const websiteConvert = (v : typeof item.recommendedCourses) => {
      var temp = new Array<LearningTree>();
      v.forEach(i => temp.push({
        name: i.webSiteName,
        value: i.webSiteUrl
      }));
      return temp;
    };
      const knowledgeConvert = (v : typeof item.knowledgePoints) => {
      var temp = new Array<LearningTree>();
      v.forEach(i => temp.push({
        name: i.knowledgePointName,
        value: i.knowledgePointContent
      }));
      return temp;
    };
    result.push({
      name: item.courseName,
      child: [
        {
          name: '可参考网站',
          child: websiteConvert(item.referenceSites)
        },
        {
          name: '推荐课程',
          child: websiteConvert(item.recommendedCourses)
        },
        {
          name: '知识点',
          child: knowledgeConvert(item.knowledgePoints)
        }
      ]
    });
  }
  return result;
}

export type User = {
  userId: string,
  userName: string,
  userPassword: string,
  userType: number
}

// Requests
const BASE_URL = 'https://localhost:7122';

export async function getCourse(id: string) {
  const response = await fetch(BASE_URL + '/course/' + id, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  });
  const result = await response.json() as Wrapper<CourseResponse>;
  return result.data;
}

export async function userRegister(data: User) {
  const response = await fetch(BASE_URL + '/user/register', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });
  const result = await response.json() as Wrapper<string>;
  return result.data;
}

export async function userLogin(id: string, password: string) {
  const response = await fetch(BASE_URL + '/user/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({UserId: id, UserPassword: password})
  });
  const result = await response.json() as Wrapper<User>;
  return result;
}


export async function addCourse(name: string, creator: string) {
  const response = await fetch(BASE_URL + '/course/addCourse', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      courseId: Date.now() % 1000,
      courseName: name,
      createTime: '2024-05-01T17:07:23',
      courseCreatorId: creator,
      courseState: 1
    })
  });
  const result = await response.json() as Wrapper<CourseResponse[number]>;
  return result;
}

export async function addWebSite(name: string, url: string, courseId: number, webSiteType: number) {
  const response = await fetch(BASE_URL + '/course/addWebSiteList', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify([{
      websiteName: name,
      webSiteType,
      websiteUrl: url,
      courseId
    }])
  });
  const result = await response.json() as Wrapper<null>;
  return result;
}

export async function addKnowledge(name: string, courseId: number, content: string) {
  const response = await fetch(BASE_URL + '/course/addKnowledgePointList', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify([{
      knowledgePointName: name,
      courseId,
      knowledgePointContent: content
    }])
  });
  const result = await response.json() as Wrapper<null>;
  return result;
}

export async function askQuestion(question: string, onMessage: (message: string) => void) {
/*   const response = await fetch(BASE_URL + '/ai/generate', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({UserQuestion: question})
  });

  const reader = response.body?.getReader();
  const utf8Decoder = new TextDecoder("utf-8");

  if (!reader) return;

  let finalText = '';  // 用于累积未完成的文本数据
  try {
    while (true) {
      const { value, done } = await reader.read();
      if (done) break;
      finalText += utf8Decoder.decode(value, {stream: true});

      let processPos = 0;  // 记录已处理数据的位置
      let eolIndex;
      while ((eolIndex = finalText.indexOf('\n', processPos)) >= 0) {
        const line = finalText.substring(processPos, eolIndex);
        processPos = eolIndex + 1;  // 更新已处理的位置到下一行的起始位置
        if (line.startsWith('data: data:')) {
          const text = line.substring(6);
          const data = JSON.parse(text.slice(5));
          if (data.output && data.output.choices[0].message.content) {
            onMessage(data.output.choices[0].message.content);
          }
        }
      }
      finalText = finalText.substring(processPos);  // 移除已处理的文本
    }
  } catch (e) {
    console.error('Error reading or processing the stream:', e);
    onMessage('解析错误');
  } */
}

export type LLMMessage = {
  role: 'system' | 'user' | 'assistant' | 'tool',
  content: string 
};

export type LLMResponse = {
  output: {
    choices: {
      finish_reason: string,
      message: LLMMessage
    }[],
    usage: {
      total_tokens: number,
      output_tokens: number,
      input_tokens: number
    },
    request_id: string
  }
}

export async function invokeLLM(messages: LLMMessage[]) {
  const url = '/api/api/v1/services/aigc/text-generation/generation';
  const apikey = 'sk-220eff910ccb4ca58a9bb2bccd6d91e1';
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': apikey
    },
    body: JSON.stringify({
      model: 'qwen-turbo',
      input: {
        messages
      },
      parameters: {
        result_format: 'message'
      }
    }),
  });
  const result = await response.json() as LLMResponse;
  return result;
}

