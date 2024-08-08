<div wire:poll.6h>
<x-slider.slider :slides="range(1, $images->count())" :duration="6000" class="h-full">
    @foreach($images as $key => $slide)
        <x-slider.slide :name="$loop->iteration" class="grid grid-cols-2 grid-rows-3 gap-4 h-full">
            @foreach($slide as $image)
                <div class="flex items-center">
                    <img class="max-h-full max-w-full m-auto" src="{{ $image['image'] }}"/>
                </div>
            @endforeach
        </x-slider.slide>
    @endforeach
</x-slider.slider>

</div>
