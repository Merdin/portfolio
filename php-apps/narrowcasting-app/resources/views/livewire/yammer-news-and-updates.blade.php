<div class="w-full h-full" wire:poll.15m>
    <x-slider.slider :slides="range(1, count($messages))" :duration="30000" class="h-full">
        @foreach($messages as $message)
            <x-slider.slide name="{{ $loop->iteration }}" class="h-full">

                @if(!empty($message['images']))
                    <x-slider.slider :slides="range(1, count($message['images']))"
                                     :duration="(30000 / count($message['images']))" class="h-[40%]">
                        @foreach($message['images'] as $image)
                            <x-slider.slide name="{{ $loop->iteration }}" image="{{ $image }}" class="h-full top-0"/>
                        @endforeach

                        @if($message['images'] > 1)
                            <x-slider.indicator class="absolute bottom-5 left-1/2"/>
                        @endif
                    </x-slider.slider>
                @endif

                <div class="@if(!empty($message['images'])) @endif p-4 text-3xl flex flex-col">
                    <div class="flex flex-row">
                        <div>
                            {{ \Carbon\Carbon::parse($message['created_at'])->format('d-m-Y') }}
                        </div>

                        <div class="ml-auto text-yammer uppercase">
                            #{{ $message['topic'] }}
                        </div>
                    </div>

                    <div class="mt-6">
                        {!! $message['content'] !!}
                    </div>
                </div>

            </x-slider.slide>
        @endforeach

        <x-slider.indicator class="absolute bottom-5 left-1/2"/>
    </x-slider.slider>
</div>
