<?php

use App\Http\Controllers\PublicSpacesController;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::prefix('screens')->group(function () {
    Route::get('/public-spaces', [PublicSpacesController::class, 'index']);
});
