<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="csrf-token" content="{{ csrf_token() }}">

    <title>{{ config('app.name', 'Laravel') }}</title>

    <link rel="stylesheet" href="{{ asset('css/app.css') }}">
    @livewireStyles

    @googlefonts
</head>

<body class="bg-gray-100 h-screen w-screen p-8">

{{ $slot }}

@livewireScripts
<script src="{{ asset('js/app.js') }}" defer></script>

</body>
</html>
