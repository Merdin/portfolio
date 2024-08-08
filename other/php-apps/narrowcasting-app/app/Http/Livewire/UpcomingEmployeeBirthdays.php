<?php

namespace App\Http\Livewire;

use App\Civi\Civi;
use Carbon\Carbon;
use Livewire\Component;

class UpcomingEmployeeBirthdays extends Component
{
    public $employees;

    public function mount()
    {
        $this->hydrate();
    }

    public function hydrate()
    {
        $response = Civi::get('GroupContact', [
            'contact_id.display_name',
            'contact_id.image_URL',
            'contact_id.birth_date',
        ], [
            'group_id' => 'Collega_s_6',
            'contact_id.birth_date' => [
                'IS NOT NULL' => 1
            ],
            'options' => [
                'limit' => 1000
            ]
        ]);

        $this->employees = collect($response['values'])->map(fn($employee) => [
            'name' => $employee['contact_id.display_name'],
            'image' => $employee['contact_id.image_URL'] ?? null,
            'birthday' => Carbon::parse($employee['contact_id.birth_date'])->setYear(null)
        ])->filter(fn($value) => Carbon::parse($value['birthday'])->greaterThanOrEqualTo(Carbon::now()->setYear(null)))
            ->sortBy('birthday')
            ->take(4)
            ->values();
    }

    public function render()
    {
        return view('livewire.upcoming-employee-birthdays');
    }
}
