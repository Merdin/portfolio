<?php


namespace App\Civi;


use Illuminate\Support\Facades\Http;

class Civi
{
    public static function get($entity, $fields = [], $conditions = [], $options = [], $sequential = true)
    {
        $userKey = config('api.civi.user_key');
        $siteKey = config('api.civi.site_key');
        $restUrl = config('api.civi.rest_url');

        if ($sequential) {
            $json['sequential'] = 1;
        }

        $json['return'] = implode(',', $fields);

        foreach ($conditions as $field => $value) {
            $json[$field] = $value;
        }

        foreach ($options as $option => $value) {
            $json['options'][] = [
                $option => $value
            ];
        }

        $json = urlencode(json_encode($json));
        $response = Http::get("{$restUrl}?entity={$entity}&action=get&api_key={$userKey}&key={$siteKey}&json={$json}")->json();

        if ($response['is_error'] == 1) {
            throw new \Exception("Civi api error: {$response['error_message']}");
        }

        return $response;
    }

    public static function create($entity, $fields, $options = []): array
    {
        $userKey = config('api.civi.user_key');
        $siteKey = config('api.civi.site_key');
        $restUrl = config('api.civi.rest_url');

        if (!empty($options)) {
            foreach ($options as $option => $value) {
                $fields['options'][$option] = $value;
            }
        }

        $json = urlencode(json_encode($fields));

        return Http::post("{$restUrl}?entity={$entity}&action=create&api_key={$userKey}&key={$siteKey}&json={$json}")->json();
    }
}
