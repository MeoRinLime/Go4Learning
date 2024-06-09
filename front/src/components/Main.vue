<template>
  <div class="flex flex-col h-full overflow-hidden">

    <section class="flex flex-1 h-[calc(100%-2.5rem)] w-auto bg-neutral-3">
      <aside class="h-full bg-white overflow-y-auto overflow-x-hidden scroll-smooth [scrollbar-width:none]">
        <div v-if="userType === '1'" @click="toggleNewCourse" class="flex justify-center items-center h-10 bg-blue hover:bg-blue-3 transition-all text-white cursor-pointer select-none">{{ showNewCourse ? '返回' : '+ 添加新课程'}}</div>
        <component :is="dynamic"  :value="list" :current="currentContent" @change="(v) => currentContent = v" />
      </aside>

      <article class="relative flex-1 h-full [scrollbar-width:none]">
        <div @click= "toggleSearch" class="flex justify-center items-center absolute bottom-8 right-8 bg-blue hover:rotate-10 hover:scale-105 transition-all rounded-full p-1.5 cursor-pointer shadow-xl">
          <Icon color="white" size="2rem"><SlideSearch24Filled /></Icon>
        </div>
        <template v-if="!showNewCourse || userType !== '1'">
          <iframe v-if="currentContent.startsWith('http')" :src="currentContent" class="h-full w-full border-none [scrollbar-width:none]"></iframe>
        <div v-else class="h-full p-6 bg-white overflow-scroll [scrollbar-width:none] " v-html="md.render(currentContent)" />
        </template>
       <div v-else class="flex flex-col gap-8 items-center h-full p-6 bg-white overflow-scroll [scrollbar-width:none] ">
          <div class="flex flex-col w-60">
            <div class="font-bold">新建课程</div>
            <div class="mt-6 text-sm text-neutral-6">名称</div>
            <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
              <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="courseName" />
            </div>
            <div @click="clickCourse" class="flex justify-center items-center self-center w-[30%] mt-2 bg-blue rounded-full p-1.5 text-white text-sm font-bold hover:shadow-lg transition-all cursor-pointer">新建</div>
          </div>

          <div v-if="courseId != 0">
            <div class="flex flex-col w-60">
              <div class="font-bold">可参考网站</div>
              <div class="mt-6 text-sm text-neutral-6">名称</div>
              <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
                <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="refWebsiteName" />
              </div>

              <div class="mt-2 text-sm text-neutral-6">URL</div>
              <div class="flex items-center  my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
                <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="refWebsiteURL" />
              </div>
              <div @click="clickRef(0)" class="flex justify-center items-center self-center w-[30%] mt-2 bg-blue rounded-full p-1.5 text-white text-sm font-bold hover:shadow-lg transition-all cursor-pointer">添加</div>
            </div>

            <div class="flex flex-col w-60">
              <div class="font-bold">参考课程</div>
              <div class="mt-6 text-sm text-neutral-6">名称</div>
              <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
                <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="refCourseName" />
              </div>

              <div class="mt-2 text-sm text-neutral-6">URL</div>
              <div class="flex items-center  my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
                <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="refCourseURL" />
              </div>
              <div @click="clickRef(1)" class="flex justify-center items-center self-center w-[30%] mt-2 bg-blue rounded-full p-1.5 text-white text-sm font-bold hover:shadow-lg transition-all cursor-pointer">添加</div>
            </div>

            
            <div class="flex flex-col w-60">
              <div class="font-bold">知识点</div>
              <div class="mt-6 text-sm text-neutral-6">名称</div>
              <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
                <input class="h-8 w-50 bg-transparent my-0 border-0 indent-4 outline-0 transition-all focus-within:w-60" v-model="knowledgeName" />
              </div>

              <div @click="clickRef(1)" class="flex justify-center items-center self-center w-[30%] mt-2 bg-blue rounded-full p-1.5 text-white text-sm font-bold hover:shadow-lg transition-all cursor-pointer">添加</div>
            </div>
          </div>
        </div>
      </article>

      <KeepAlive>
        <Search v-if="showSearch" />
      </KeepAlive>
    </section>

  </div>
</template>

<script setup lang="ts">
import { ref, onBeforeMount } from "vue";
import { SlideSearch24Filled } from "@vicons/fluent";
import { Icon } from "@vicons/utils";
import Message from "vue-m-message";
import MarkdownIt from "markdown-it";


import Aside from "./Aside.vue"
import Search from "./Search.vue"
import { LearningTree, addCourse, addKnowledge, addWebSite, getCourse, toLearningTree } from "../api";
import type { User } from "../api";
import { useRoute } from "vue-router";

const md = new MarkdownIt();

const userid  = ref('');
const userType = ref('');

const currentContent = ref('');
const showSearch = ref(false);
const showNewCourse = ref(false);

const courseName = ref('');
const courseId = ref(0);
const refWebsiteName = ref('');
const refWebsiteURL = ref('');
const refCourseName = ref('');
const refCourseURL = ref('');
const knowledgeName = ref('');

const list = ref([] as LearningTree[]);


const dynamic = ref<any>();

const toggleSearch = () => 
  showSearch.value = !showSearch.value;


const toggleNewCourse = () => 
  showNewCourse.value = !showNewCourse.value;

const clickCourse = () => 
  addCourse(courseName.value, userid.value)
  .then((r) => {
    courseId.value = r.data.courseId;
    flash()
  });

const clickRef = (refType: number) => 
  addWebSite(refWebsiteName.value, refWebsiteURL.value, courseId.value, refType)
  .then(() => flash());

const clickKnowledge = (refType: number) => 
  addKnowledge(knowledgeName.value, courseId.value, vditor.value?.getValue()!)
  .then(() => flash());


const flash = () => {
  getCourse(userType.value === "1" ? `${userid.value}` : "-1").then((data) => {
    list.value = toLearningTree(data);
    dynamic.value = Aside;
    console.log(list.value);
  }).catch(() => 
    Message.error('无法连接至服务器')
  );
}

onBeforeMount(() => {
  flash();
  let route = useRoute();
  userid.value = route.query.id?.toString()!;
  console.log(userid.value);
  userType.value = route.query.type?.toString()!;
  console.log(userType.value) 
})


</script>
