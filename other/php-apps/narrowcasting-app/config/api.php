<?php

return [
    'civi' => [
        'site_key' => env('CIVICRM_SITE_API_KEY', ''),
        'user_key' => env('CIVICRM_USER_API_KEY', ''),
        'rest_url' => env('CIVI_REST_URL', ''),
    ],
    'open_weather_map' => [
        'key' => env('OPENWEATHERMAP_API_KEY', ''),
        'zwolle_lat' => '52.5125',
        'zwolle_lon' => '6.09444'
    ]
];
