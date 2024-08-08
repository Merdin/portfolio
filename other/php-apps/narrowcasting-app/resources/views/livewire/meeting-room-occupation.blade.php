<div class="py-4 flex flex-col gap-y-4" wire:poll.5m>
    @foreach($availability as $item)
        <div
            class="flex flex-row px-4 py-2 text-white text-2xl @if(empty($item['name'])) bg-green-600 @else bg-red-500 @endif">
            <div>
                {{ $item['from'] }} - {{ $item['to'] }}
            </div>

            <div class="ml-auto">
                {{ $item['name'] ?? 'Vrij' }}
            </div>
        </div>
    @endforeach
</div>
