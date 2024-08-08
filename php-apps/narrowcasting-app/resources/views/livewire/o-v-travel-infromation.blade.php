<div class="h-full w-full grid grid-rows-3 divide-y-2 overflow-auto" wire:poll.5m>
    @if($departures != null)
        @foreach($departures as $departure)
        <div class="row-span-1 w-full flex flex-row items-center gap-x-4 py-5 px-4">
        @if($departure['type'] != "train")
                <x-tabler-train class="h-20 w-20"/>
            @else
                <x-tabler-bus class="h-20 w-20"/>
            @endif
            <span class="row text-xl">

              {{ $departure['direction'] }} {{ $departure['plannedDateTime'] }}
              @if($departure['delay'] != 0)
              + {{ $departure['delay'] }}
              @endif
            </span>
        </div>
    @endforeach
   @else
   <h1>er zijn geen vertragingen</h1>
   @endif

</div>

