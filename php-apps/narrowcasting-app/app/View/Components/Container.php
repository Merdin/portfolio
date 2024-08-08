<?php

namespace App\View\Components;

use Illuminate\View\Component;

class Container extends Component
{
    public $title;

    public $background;

    /**
     * Create a new component instance.
     *
     * @return void
     */
    public function __construct($title = null, $background = true)
    {
        $this->title = $title;
        $this->background = $background;
    }

    /**
     * Get the view / contents that represent the component.
     *
     * @return \Illuminate\Contracts\View\View|\Closure|string
     */
    public function render()
    {
        return view('components.container');
    }
}
