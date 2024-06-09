<template>
  <div class="flex flex-col justify-center h-full items-center bg-white">
    <img src="../assets/logo.svg" class="w-16 h-16" />
    <div class="mb-4 text-xl text-blue-5 font-bold">Go4Learning</div>
    <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
      <Icon size="1.5rem" color="rgb(59 130 246)"><Person16Filled /></Icon>
      <input class="h-8 w-50 bg-transparent my-0 border-0 outline-0 indent-4 transition-all focus-within:w-60" v-model="username" />
    </div>
    <div class="flex items-center my-2 px-2 border-solid border-px border-neutral-3 transition-all focus-within:border-blue bg-white rounded-full">
      <Icon size="1.5rem" color="rgb(59 130 246)"><LockClosed16Filled /></Icon>
      <input class="h-8 w-50 bg-transparent my-0 border-0 outline-0 indent-4 transition-all focus-within:w-60" v-model="password" />
    </div>
    <div v-if="!isLogin" class="flex items-center my-2 w-60 text-xs">
      我是
      <input type="radio" id="student" name="usertype" :value="0" v-model="userType" checked />
      <label for="student">学生</label>
      <input type="radio" id="teacher" name="usertype" :value="1" v-model="userType" />
      <label for="student">老师</label>
    </div>
    <div v-if="isLogin" class="text-xs w-60 select-none">没有账号？<a @click="isLogin = !isLogin" class="text-blue cursor-pointer">立即注册</a></div>
    <div v-else class="text-xs w-60 select-none"><a @click="isLogin = !isLogin" class="text-blue cursor-pointer">返回登录</a></div>
    <button @click="submit" class="h-10 w-60 mt-6 border-none cursor-pointer select-none bg-blue-5 hover:bg-blue-3 active:bg-blue transition-all appearance-none rounded-lg text-base text-white">{{isLogin ? '登录' : '注册'}}</button>
  </div>
</template>

<script setup lang="ts">
import { WebviewWindow, getCurrent } from '@tauri-apps/api/webviewWindow';
import { ref } from 'vue';
import { userLogin, userRegister } from '../api';
import Message from 'vue-m-message';
import { Person16Filled, LockClosed16Filled } from '@vicons/fluent';
import { Icon } from '@vicons/utils';

const username = ref('');
const password = ref('');
const isLogin = ref(true);
const userType = ref(0)

function submit() {
  const jumpToMain = () => {
    const webview = new WebviewWindow('mainWebview', {
      url: `main?id=${username.value}&type=${userType.value}`,
      center: true
    });
    webview.once('tauri://created', async function () {
      // webview successfully created
      await getCurrent().close();
    });
    webview.once('tauri://error', function () {
      // an error happened creating the webview
      Message.error('内部错误');
    });
  }
  if ( username.value === '@') {
    jumpToMain();
  };
  
  if (isLogin.value) 
    userLogin(username.value, password.value)
    .then((r) => {
      userType.value = r.data.userType;
      jumpToMain();
    })
    .catch(() => Message.error('无法连接至服务器'));
  else 
    userRegister({
      UserId: username.value,
      UserName: username.value,
      UserPassword: password.value,
      UserType: userType.value
    })
    .then(() => {
      username.value = '';
      password.value = '';
      isLogin.value = true;
      Message.success('注册成功');
    })
    .catch(() => Message.error('无法连接至服务器'));
}

</script>