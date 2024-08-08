<?php

namespace App\Http\Livewire;

use App\Civi\Civi;
use Livewire\Component;

class Brands extends Component
{
    public object|array $images = [];

    public function mount()
    {
        $this->hydrate();

    }

    public function hydrate()
    {
        $data = Civi::get('GroupContact', [
            'contact_id.image_URL',
        ], [
            'group_id' => 'Merken_platformen_8',
        ]);

        foreach ($data['values'] as $key => $image) {
            if (isset($image["contact_id.image_URL"])) {
                $images[] = [
                    'id' => $image['id'],
                    'image' => $image["contact_id.image_URL"],
                ];
            }
        }

        $this->images = collect($images)->chunk(6);
    }

    public function render()
    {
        return view('livewire.brands');
    }
}
