<?php

namespace App\View\Components\Slider;

use Illuminate\View\Component;

class Slide extends Component
{
    public $name;
    public $image;
    public $animation;

    /**
     * Create a new component instance.
     *
     * @return void
     */
    public function __construct($name, $image = null, $animation = 'pop')
    {
        $this->name = $name;
        $this->image = $image;
        $this->animation = $animation;
    }

    /**
     * Get the view / contents that represent the component.
     *
     * @return \Illuminate\Contracts\View\View|\Closure|string
     */
    public function render()
    {
        return view('components.slider.slide');
    }
}
