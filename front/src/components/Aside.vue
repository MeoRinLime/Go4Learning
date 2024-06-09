<template>
  <ul class="flex flex-col transition-all duration-500 overflow-hidden w-60 box-border m-none p-none list-none">
    <li v-for="(item, index) of value" class="w-full">
      <header @click.stop="onClickHeader(item, index)" class="flex items-center h-10 pr-3 box-border cursor-pointer hover:bg-white hover:shadow-xl transition-all border-blue border-solid border-0" 
      :style="{'padding-left': `${depth + 0.5}rem`}" :class="{'bg-white': depth === 0, 'bg-neutral-1': depth !== 0, 'border-r-4 bg-white': item.value === current}">
        <Icon v-if="depth === 0" color="blue"><Navigation16Filled /></Icon>
        <div class="pl-2 text-sm select-none whitespace-nowrap text-ellipsis overflow-hidden" :class="{'text-blue-5': depth <= 1}"> {{ item.name }}</div>
        <Icon v-if="item.child" class="ml-auto transition" :class="{ 'rotate-180' : !collapse[index]}" :color="depth <= 1 ? 'blue' : 'black'"><ChevronUp16Filled /></Icon>
      </header>
      <Transition name="aside" enter-from-class="max-h-0" enter-to-class="max-h-screen" 
      leave-from-class="max-h-screen" leave-to-class="max-h-0">
        <Aside v-if="item.child" v-show="!collapse[index]" :value="item.child" :depth="depth + 1" :current="current" @change="(v) => emit('change', v)"/>
      </Transition>
    </li>
  </ul>
</template>

<script setup lang="ts">
import { ChevronUp16Filled, Navigation16Filled } from "@vicons/fluent";
import { Icon } from "@vicons/utils";
import type { LearningTree } from "../api";
import { reactive } from "vue";

const { value, depth } = withDefaults(
  defineProps<{
    value: LearningTree[], 
    depth?: number,
    current: string
  }>(), 
  {
    depth: 0
  }
);
const emit = defineEmits<{(e: 'change', value: string): void}>();

const collapse = reactive(new Array<boolean>(value.length));
collapse.fill(false);

const onClickHeader = (item: LearningTree, index: number) => {
  collapse[index] = !collapse[index];
  if (item.value) emit('change', item.value);
}

</script>

<style>
</style>