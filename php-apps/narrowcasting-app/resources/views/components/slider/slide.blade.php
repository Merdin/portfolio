<div {{ $attributes->merge(['class' => 'absolute w-full']) }} x-show="current == {{ $name }}"
     @if($animation == 'pop')
         x-transition:enter="transition ease-out duration-300"
     x-transition:enter-start="opacity-0 transform scale-90"
     x-transition:enter-end="opacity-100 transform scale-100"
     x-transition:leave="transition ease-in duration-300"
     x-transition:leave-start="opacity-100 transform scale-100"
     x-transition:leave-end="opacity-0 transform scale-90"
     @else
         x-transition:enter="transition duration-1000"
     x-transition:enter-start="transform translate-x-full"
     x-transition:enter-end="transform translate-x-0"
     x-transition:leave="transition duration-1000"
     x-transition:leave-start="transform"
     x-transition:leave-end="transform -translate-x-full"
    @endif
>

    @isset($image)
        <div class="w-full h-full bg-no-repeat bg-center bg-cover"
             style="background-image: url('{{ $image }}')">
        </div>
    @endisset

    {{ $slot ?? '' }}
</div>

