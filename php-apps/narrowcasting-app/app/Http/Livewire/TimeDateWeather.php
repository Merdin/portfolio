<?php

namespace App\Http\Livewire;

use Illuminate\Support\Carbon;
use Livewire\Component;

class TimeDateWeather extends Component
{
    public $time;
    public $date;

    public function mount()
    {
        $this->hydrate();
    }

    public function hydrate()
    {
        $this->time = Carbon::now()->format('H:i');
        $this->date = Carbon::now()->translatedFormat('D d-m');
    }

    public function render()
    {
        return view('livewire.time-date-weather');
    }
}
