import '../models/house_response.dart';
import '../repositories/house_repository.dart';
import 'package:rxdart/rxdart.dart';

class HousesListBloc {
  final HouseRepository _houseRepository = HouseRepository();
  final BehaviorSubject<HouseResponse> _subject =
      BehaviorSubject<HouseResponse>();

  getHouses() async {
    HouseResponse response = await _houseRepository.getHouses();
    _subject.sink.add(response);
  }

  dispose() {
    _subject.close();
  }

  BehaviorSubject<HouseResponse> get subject => _subject;
}

final housesBloc = HousesListBloc();
