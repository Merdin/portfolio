<div class="w-full h-full" wire:poll.1h>
    <x-slider.slider :slides="range(1, count($feed))" :duration="30000" class="h-full overflow-hidden">
        @foreach($feed as $item)
            <x-slider.slide name="{{ $loop->iteration }}" class="h-full">
                <div class="w-full h-[40%] bg-no-repeat bg-center bg-cover rounded"
                     style="background-image: url('{{ $item->get_enclosure()->get_link() }}')">
                </div>

                <div class="px-4 py-2 h-60% flex flex-col">
                    <h1 class="text-4xl font-semibold"> {{ $item->get_title() }} </h1>
                    <p class="py-4 text-2xl">{{ $item->get_description() }}</p>
                    <div class="w-full absolute bottom-0 bg-white h-[3.75rem] rounded"></div>
                </div>
            </x-slider.slide>
        @endforeach
        <x-slider.indicator class="absolute bottom-5 left-1/2"/>
    </x-slider.slider>
</div>
