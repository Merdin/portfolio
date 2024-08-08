<?php

namespace App\Http\Livewire;

use Livewire\Component;

class NosRssFeed extends Component
{
    /*
     * Documentation for the package we use to read the RSS Feed: https://github.com/willvincent/feeds
     * API documentation for the package: http://simplepie.org/wiki/reference/start
     */

    private $data;

    public function mount()
    {
        $this->hydrate();
    }

    public function hydrate() {
        $feed = \Feeds::make('http://feeds.nos.nl/nosnieuwsalgemeen');
        $feed->strip_htmltags(array_merge($feed->strip_htmltags, array('h1', 'h2', 'a', 'img', 'p')));

        $this->data = array(
            'title'     => $feed->get_title(),
            'permalink' => $feed->get_permalink(),
            'feed'     => $feed->get_items(0, 3),
        );
    }

    public function render()
    {
        return view('livewire.nos-rss-feed', $this->data);
    }
}
