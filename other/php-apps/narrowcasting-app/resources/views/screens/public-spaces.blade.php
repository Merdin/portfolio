<x-layout>
    <x-slider.slider :slides="['1','2']" :duration="90000" class="h-full">
        <x-slider.slide name="1" class="h-full" animation="slide">
            @include('layouts.organization-wide')
        </x-slider.slide>


        <x-slider.slide name="2" class="h-full" animation="slide">
            @include('layouts.public-information')
        </x-slider.slide>
    </x-slider.slider>
</x-layout>
