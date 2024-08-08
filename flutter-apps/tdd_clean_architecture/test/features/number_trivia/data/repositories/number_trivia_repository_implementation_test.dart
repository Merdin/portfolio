import 'package:mockito/mockito.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tdd_clean_architecture/core/platform/network_info.dart';
import 'package:tdd_clean_architecture/features/number_trivia/data/datasources/number_trivia_local_data_source.dart';
import 'package:tdd_clean_architecture/features/number_trivia/data/datasources/number_trivia_remote_data_source.dart';
import 'package:tdd_clean_architecture/features/number_trivia/data/models/number_trivia_model.dart';
import 'package:tdd_clean_architecture/features/number_trivia/data/repositories/number_trivia_repository_implementation.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/entities/number_trivia.dart';

class MockRemoteDataSource extends Mock
    implements NumberTriviaRemoteDataSource {}

class MockLocalDataSource extends Mock implements NumberTriviaLocalDataSource {}

class MockNetworkInfo extends Mock implements NetworkInfo {}

void main() {
  final mockRemoteDataSource = MockRemoteDataSource();
  final mockLocalDataSource = MockLocalDataSource();
  final mockNetworkInfo = MockNetworkInfo();
  final repository = NumberTriviaRepositoryImplementation(
    remoteDataSource: mockRemoteDataSource,
    localDataSource: mockLocalDataSource,
    networkInfo: mockNetworkInfo,
  );

  group('getConcreteNumberTrivia', () {
    const number = 1;
    const numberTriviaModel =
        NumberTriviaModel(number: number, text: 'test trivia');
    const NumberTrivia numberTrivia = numberTriviaModel;

    test('should check if the device is online', () async {
      when(mockNetworkInfo.isConnected).thenAnswer((_) async => true);

      repository.getConcreteNumberTrivia(number);

      verify(mockNetworkInfo.isConnected);
    });
  });
}
