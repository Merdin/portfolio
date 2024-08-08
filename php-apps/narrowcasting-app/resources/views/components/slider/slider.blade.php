<div {{ $attributes->merge(['class' => 'relative']) }} x-data="{
            current: {{ $current }},
            slides: {{ json_encode($slides) }},
            loop() {
                setInterval(() => {
                    if (this.current == this.slides[this.slides.length - 1]) {
                        this.current = this.slides[0];
                    } else {
                        this.slides.every((element, index) => {
                            if (element == this.current) {
                                   this.current = this.slides[index + 1];
                                   return false;
                                }
                            return true;
                        });
                    }
                }, {{ $duration }});
            }
}" x-init="loop();">
    {{ $slot }}
</div>
