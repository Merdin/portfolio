<?php

namespace App\Http\Livewire;

use Carbon\Carbon;
use Livewire\Component;

class MeetingRoomOccupation extends Component
{
    public $availability;

    public function mount()
    {
        $this->availability = collect([
            [
                'from' => Carbon::parse('8:30')->format('H:i'),
                'to' => Carbon::parse('10:00')->format('H:i'),
                'name' => null
            ],
            [
                'from' => Carbon::parse('10:00')->format('H:i'),
                'to' => Carbon::parse('12:00')->format('H:i'),
                'name' => 'Coos Walinga'
            ],
            [
                'from' => Carbon::parse('12:00')->format('H:i'),
                'to' => Carbon::parse('14:00')->format('H:i'),
                'name' => 'Harald Jansen'
            ],
            [
                'from' => Carbon::parse('14:00')->format('H:i'),
                'to' => Carbon::parse('17:00')->format('H:i'),
                'name' => null
            ],
        ]);
    }

    public function render()
    {
        return view('livewire.meeting-room-occupation');
    }
}
