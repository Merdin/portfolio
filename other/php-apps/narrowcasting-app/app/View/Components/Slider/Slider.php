<?php

namespace App\View\Components\Slider;

use Illuminate\View\Component;

class Slider extends Component
{
    public $slides;
    public $current;
    public $duration;

    /**
     * Create a new component instance.
     *
     * @return void
     */
    public function __construct($slides, $current = null, $duration = 3000)
    {
        $this->slides = $slides;
        $this->current = $current ?? $slides[0];
        $this->duration = $duration;
    }

    /**
     * Get the view / contents that represent the component.
     *
     * @return \Illuminate\Contracts\View\View|\Closure|string
     */
    public function render()
    {
        return view('components.slider.slider');
    }
}
