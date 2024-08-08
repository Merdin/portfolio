import 'package:dartz/dartz.dart';
import 'package:mockito/mockito.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/entities/number_trivia.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/repositories/number_trivia_repository.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tdd_clean_architecture/features/number_trivia/domain/usecases/get_random_number_trivia.dart';

class MockNumberTriviaRepository extends Mock
    implements NumberTriviaRepository {}

void main() {
  test('should get random trivia from the repository', () async {
    const numberTrivia = NumberTrivia(text: 'test', number: 1);

    var mockNumberTriviaRepository = MockNumberTriviaRepository();
    var usecase = GetRandomNumberTrivia(mockNumberTriviaRepository);
    // arrange
    when(mockNumberTriviaRepository.getRandomNumberTrivia())
        .thenAnswer((_) async => const Right(numberTrivia));

    // act
    final result = await usecase();

    // assert
    expect(result, const Right(numberTrivia));
    verify(mockNumberTriviaRepository.getRandomNumberTrivia());
    verifyNoMoreInteractions(mockNumberTriviaRepository);
  });
}
