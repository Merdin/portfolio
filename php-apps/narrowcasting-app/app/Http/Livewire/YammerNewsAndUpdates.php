<?php

namespace App\Http\Livewire;

use Illuminate\Support\Facades\Http;
use Livewire\Component;

class YammerNewsAndUpdates extends Component
{
    public array $messages = [];

    public function mount()
    {
        $response = Http::withToken('212857-G3FLeuczHMAImrmGebYig')->get('https://www.yammer.com/api/v1/messages/in_group/eyJfdHlwZSI6Ikdyb3VwIiwiaWQiOiIxMTA4MDk3NTE1NTIifQ.json')->json();

        foreach ($response['messages'] as $key => $message) {
            $thread = Http::withToken('212857-G3FLeuczHMAImrmGebYig')->get('https://www.yammer.com/api/v1/threads/1743704643706880.json?limit=3')->json();

            $topic = null;
            foreach ($thread['references'] as $reference) {
                if ($reference['type'] === 'topic') {
                    $topic = $reference['name'];
                }
            }

            $messages[$key] = [
                'topic' => $topic,
                'content' => $message['body']['rich'],
                'created_at' => $message['created_at'],
                'images' => []
            ];

            $count = 0;
            foreach ($message['attachments'] as $attachment) {
                if ($attachment['type'] === 'image') {
                    $messages[$key]['images'][] = $attachment['image']['url'];

                    if ($count === 2) {
                        break;
                    }

                    $count++;
                }
            }
        }

        $this->messages = $messages ?? [];
    }

    public function render()
    {
        return view('livewire.yammer-news-and-updates');
    }
}
