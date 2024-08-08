<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class PublicSpacesController extends Controller
{
    public function index()
    {
        return view('screens.public-spaces');
    }
}
