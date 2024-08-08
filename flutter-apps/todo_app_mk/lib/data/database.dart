import 'package:hive/hive.dart';

class TodoDatabase {
  List todoList = [];
  final box = Hive.box('custom_box');

  void createInitialData() {
    todoList = [
      ["Make Tutorial", false],
      ["Do Exercise", false],
    ];
  }

  void loadData() {
    todoList = box.get("TODOLIST");
  }

  void updateData() {
    box.put("TODOLIST", todoList);
  }
}
