import 'package:dartz/dartz.dart';
import 'package:mockito/mockito.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/entities/number_trivia.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/repositories/number_trivia_repository.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/usecases/get_concrete_number_trivia.dart';

class MockNumberTriviaRepository extends Mock
    implements NumberTriviaRepository {}

void main() {
  test('should get trivia for the number from the repository', () async {
    const number = 1;
    const numberTrivia = NumberTrivia(text: 'test', number: 1);

    var mockNumberTriviaRepository = MockNumberTriviaRepository();
    var usecase = GetConcreteNumberTrivia(mockNumberTriviaRepository);
    // arrange
    when(mockNumberTriviaRepository.getConcreteNumberTrivia(number))
        .thenAnswer((_) async => const Right(numberTrivia));

    // act
    final result = await usecase(const Params(number: number));

    // assert
    expect(result, const Right(numberTrivia));
    verify(mockNumberTriviaRepository.getConcreteNumberTrivia(number));
    verifyNoMoreInteractions(mockNumberTriviaRepository);
  });
}
