<div class="flex flex-row w-full h-full gap-6">
    <div class="grid grid-cols-2 gap-6 h-full grow">
        <x-container title="Nieuws" class="col-span-1 w-full">
            <x-slot name="badges">
                <x-badge class="text-white bg-red-700">
                    NOS
                </x-badge>
            </x-slot>

            @livewire('nos-rss-feed')
        </x-container>

        <div class="col-span-1 grid grid-rows-2 gap-y-6 h-full">
            <div class="row-span-1 w-full">
                <x-container title="Merken" :background="false" class="w-full h-full">
                    @livewire('brands')
                </x-container>
            </div>

            <div class="row-span-1 whitespace-nowrap">
                <x-container title="Vertragingen">
                    @livewire('o-v-travel-infromation')
                </x-container>
            </div>
        </div>
    </div>

    <div class="gap-y-6 flex flex-col">
        <div class="flex grow-0 whitespace-nowrap ml-auto">
            @livewire('time-date-weather')
        </div>

        <div class="flex flex-grow">
            @livewire('buienradar')
        </div>
    </div>
</div>
