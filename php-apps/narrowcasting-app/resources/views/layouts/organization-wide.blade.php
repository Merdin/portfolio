<div class="grid grid-cols-10 w-full h-full gap-6">
    <div class="col-span-6">
        <x-container title="Nieuws & updates">
            <x-slot name="badges">
                <x-badge class="text-white bg-yammer">
                    Yammer
                </x-badge>
            </x-slot>
            @livewire('yammer-news-and-updates')
        </x-container>
    </div>

    <div class="col-span-4 grid grid-rows-2 gap-y-6">
        <div class="flex flex-row row-span-1 gap-4">
            <div class="flex flex-grow">
                <x-container title="Verjaardagen">
                    @livewire('upcoming-employee-birthdays')
                </x-container>
            </div>

            <div class="flex grow-0 whitespace-nowrap">
                @livewire('time-date-weather')
            </div>
        </div>
        <div class="row-span-1">
            <x-container title="Vergaderruimte">
                @livewire('meeting-room-occupation')
            </x-container>
        </div>
    </div>
</div>
