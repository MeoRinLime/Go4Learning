<template>
  <aside class="flex flex-col items-center h-full w-80 box-border bg-neutral-1 [scrollbar-width:none]">
    <div class="flex flex-col flex-1 w-[calc(100%-3rem)] px-4 py-6 gap-4 overflow-scroll [scrollbar-width:none]">
      <div v-for="i in chatHistory" class="shadow-xl p-2 rounded-lg max-w-[80%]" v-html="md.render(i.content)"
        :class="i.role === 'user' ? 'bg-sky text-white self-end rounded-tr-none' : 'bg-white self-start rounded-tl-none' " />
    </div>
    <input v-model="keyword" @keyup.enter="submit(keyword)" placeholder="问点什么"
      class="h-8 w-[80%] mb-6 bg-neutral-2 rounded-full indent-4 appearance-none border-none focus-visible:outline-none" />
    <div id="bubble" @click="submit('请解释' + bubbleKeyword)" class="flex justify-center items-center fixed bg-blue h-8 w-8 rounded-full shadow-lg cursor-pointer" style="visibility: hidden;">
      <Icon color="white" size="1.5rem"><ChatBubblesQuestion16Filled /></Icon>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import { ChatBubblesQuestion16Filled } from "@vicons/fluent";
import { Icon } from "@vicons/utils";
import MarkdownIt from "markdown-it";
import Message from "vue-m-message";
import { invokeLLM } from "../api"
import type { LLMMessage } from "../api"

const keyword = ref('');
const showBubble = ref(false);
const bubbleKeyword = ref('');

const chatHistory = ref(new Array<LLMMessage>());
const md = new MarkdownIt();

function submit(v: string | null) {
  if (v !== null) {
    chatHistory.value.push({
      role: 'user',
      content: v
    });
    keyword.value = '';
  }

  invokeLLM([{
      role: 'system',
      content: '你是一个专注于计算机领域的对话助手。'
    }, ...chatHistory.value]
  ).then((r) => 
    chatHistory.value.push(r.output.choices[0].message)
  ).catch(() => 
    Message.error('无法连接至服务器')
  );
};

onMounted(() => {
  submit('你好');

  window.addEventListener('mouseup', (e) => {
    let selection = document.getSelection()?.toString();
    let bubble = document.getElementById('bubble');
    if (selection !== undefined && selection !== '') {
      bubble?.setAttribute('style', `left: ${e.pageX}px; top: ${e.pageY}px; visibility: visible;`)
      showBubble.value = true;
      bubbleKeyword.value = selection;
    } else {
      bubble?.setAttribute('style', 'visibility: hidden;');
      showBubble.value = false;
    }
  });
});

</script>