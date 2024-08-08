<div class="flex flex-col justify-items-end" wire:poll>
     <span class="ml-auto font-semibold
      text-6xl">
         {{ $time }}
     </span>

    <span class="ml-auto text-3xl capitalize">
        {{ $date }}
    </span>

    <span class="ml-auto text-3xl">
        @livewire('daily-weather')
    </span>
</div>
