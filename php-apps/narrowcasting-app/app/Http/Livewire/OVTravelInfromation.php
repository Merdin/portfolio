<?php

namespace App\Http\Livewire;
use Illuminate\Support\Facades\Http;
use Livewire\Component;
use DateTime;

class OVTravelInfromation extends Component
{
    public $procesedDepartures;
    public array $departures = [];

    public function mount()
    {

        $response = Http::withHeaders([
            'Ocp-Apim-Subscription-Key' => 'fcf1a9030ff64c838e5e45b386698af0'
        ])->get('https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/departures?lang=nl&station=zl')->json();
        $data = $response['payload'];
        foreach ($data['departures'] as $key => $traindeparture) {
            if($traindeparture['plannedDateTime'] != $traindeparture['actualDateTime'])
            {
                //date( "H:i", strtotime($departure['plannedDateTime']))
                $plannedtime = new DateTime($traindeparture['plannedDateTime']);
                $actualplannedtime = new DateTime($traindeparture['actualDateTime']);
                $delay = $plannedtime->diff($actualplannedtime);
                $this->departures[] = [
                    'type' => "train",
                    'direction' => $traindeparture['direction'],
                    'plannedTrack' => $traindeparture['plannedTrack'],
                    'plannedDateTime' => $plannedtime->format('H:i'),
                    'delay' => $delay->format('%i'),
                ];
            };
        };
            $this->departures;
        $ovapi = http::get('http://v0.ovapi.nl/tpc/46120160/')->json();
        $data = $ovapi['46120160'];
        foreach ($data['Passes'] as $key => $busdeparture) {
            if($busdeparture['TargetArrivalTime'] != $busdeparture['ExpectedArrivalTime'])
            {
                //date( "H:i", strtotime($departure['plannedDateTime']))
                $plannedtime = new DateTime($busdeparture['TargetArrivalTime']);
                $actualplannedtime = new DateTime($busdeparture['ExpectedArrivalTime']);

                $delay = $plannedtime->diff($actualplannedtime);
                $this->departures[] = [
                    'type' => "bus",
                    'direction' => $busdeparture['DestinationName50'],
                    'plannedDateTime' => $plannedtime->format('H:i'),
                    'delay' => $delay->format('%i'),
                ];
            };
        };

        $this->departures = array_chunk($this->departures, 3)[0] ?? [];
    }

    public function render()
    {
        return view('livewire.o-v-travel-infromation');
    }
}
