import 'house.dart';

class HouseResponse {
  final List<House> houses;
  final String? error;

  HouseResponse(this.houses, this.error);

  HouseResponse.fromJson(List<dynamic> json)
      : houses = (json).map((i) => House.fromJson(i)).toList(),
        error = null;

  HouseResponse.withError(String errorValue)
      : houses = [],
        error = errorValue;

  House getHouse(int id) {
    return houses.firstWhere((house) => house.id == id);
  }
}

extension Sorting on List<House> {
  void get sortByPrice => sort((a, b) => a.price.compareTo(b.price));
}

extension Filtering on HouseResponse {
  List<House> filter(String text) => houses
      .where((house) =>
          house.city.containsIgnoreCase(text) ||
          house.zip.containsIgnoreCase(text))
      .toList();
}

extension StringExtensions on String {
  bool containsIgnoreCase(String secondString) =>
      toLowerCase().contains(secondString.toLowerCase());

  //bool isNotBlank() => this != null && this.isNotEmpty;
}
