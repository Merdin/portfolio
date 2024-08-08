<div class="h-full w-full grid grid-rows-4 divide-y-2" wire:poll.4h>
    @foreach($employees as $employee)
        <div class="row-span-1 w-full flex flex-row items-center gap-x-4 py-2 px-4">
            @if(!empty($employee['image']))
                <img src="{{ $employee['image'] }}" class="h-16 w-16 rounded-full">
            @else
                <div class="h-16 w-16 rounded-full bg-gray-300"></div>
            @endif

            <span class="text-3xl">
                {{ $employee['name'] }}
            </span>

                <span class="ml-auto text-3xl whitespace-nowrap">
                {{ \Carbon\Carbon::parse($employee['birthday'])->format('d-m') }}
            </span>
        </div>
    @endforeach
</div>
