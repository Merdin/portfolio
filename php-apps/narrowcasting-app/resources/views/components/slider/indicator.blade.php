<div {{ $attributes->merge(['class' => 'flex gap-x-4 -translate-x-1/2']) }}>
    <template x-for="slide in slides">
        <div x-data class="w-5 h-5 rounded-full border-white"
             :class="(current == slide) ? 'bg-gray-800' : 'bg-gray-400'"></div>
    </template>
</div>
