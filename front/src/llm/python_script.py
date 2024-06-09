import sys
import io
from dashscope import Generation
from http import HTTPStatus

sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

def get_response_stream(messages):
    responses = Generation.call(
        model="qwen-turbo",
        messages=messages,
        result_format='message',
        stream=True,
        incremental_output=True
    )
    return responses

def main(question):
    messages = [{'role': 'system', 'content': '你是一个专注于计算机领域的对话助手。'}]
    
    if question.startswith("解释"):
        term = question.replace("解释", "").strip()
        explanation_message = [{'role': 'user', 'content': f"请解释一下：{term}"}]
        explanation_stream = get_response_stream(explanation_message)
        assistant_output = ""

        for response in explanation_stream:
            if response.status_code == HTTPStatus.OK:
                for choice in response.output['choices']:
                    part = choice['message']['content']
                    assistant_output += part
            else:
                assistant_output = 'Error: %s, %s' % (response.status_code, response.message)
                break

        print(assistant_output)
    else:
        messages.append({'role': 'user', 'content': question})
        response_stream = get_response_stream(messages)
        assistant_output = ""

        for response in response_stream:
            if response.status_code == HTTPStatus.OK:
                for choice in response.output['choices']:
                    part = choice['message']['content']
                    assistant_output += part
            else:
                assistant_output = 'Error: %s, %s' % (response.status_code, response.message)
                break

        print(assistant_output)

if __name__ == "__main__":
    question = sys.argv[1]
    main(question)
