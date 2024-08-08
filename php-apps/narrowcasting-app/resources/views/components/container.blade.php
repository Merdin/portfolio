<div class="flex flex-col gap-y-6 h-full w-full">
    <div class="flex flex-row gap-x-4 items-center">
        <span class="text-5xl">
            {{ $title }}
        </span>

        @isset($badges)
            <div>
                {{ $badges }}
            </div>
        @endisset
    </div>

    <div class="@if($background) shadow-xl bg-white @endif h-full rounded">
        {{ $slot }}
    </div>
</div>
