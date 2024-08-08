<?php

namespace App\Http\Livewire;

use Livewire\Component;
use Illuminate\Support\Facades\Http;

class DailyWeather extends Component
{
    public $temp;
    public $iconCode; // For more information about the icons we retrieve  https://openweathermap.org/weather-conditions

    public function mount()
    {
        $this->hydrate();
    }

    public function hydrate()
    {
        $response = $this->getWeather();

        $mainData = $response['main'];
        // Get temperature & round
        $this->temp = round($mainData['temp']);

        $weatherData = $response['weather'][0];
        // Get temperature icon
        $this->iconCode = $weatherData['icon'];

    }

    /**
     * For more information about the response: https://openweathermap.org/current#current_JSON
     *
     * @return array|mixed
     */
    private function getWeather(): mixed
    {
        $apiKey = config('api.open_weather_map.key');
        $lat = config('api.open_weather_map.zwolle_lat');
        $lon = config('api.open_weather_map.zwolle_lon');

        $response = HTTP::get("https://api.openweathermap.org/data/2.5/weather?lat={$lat}&lon={$lon}&appid={$apiKey}&lang=nl&units=metric")->json();
        return $response;
    }

    public function render()
    {
        return view('livewire.daily-weather');
    }
}
