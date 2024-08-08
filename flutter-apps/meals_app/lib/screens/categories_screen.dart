import 'package:flutter/material.dart';
import 'package:flutter_complete_guide/widgets/category_item.dart';

import '../models/dummy_data.dart';

class CategoriesScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GridView(
      padding: EdgeInsets.all(25),
      gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
        maxCrossAxisExtent: 200,
        childAspectRatio: 1.5,
        crossAxisSpacing: 20,
        mainAxisSpacing: 20,
      ),
      children: DUMMY_CATEGORIES
          .map((data) => CategoryItem(data.id, data.title, data.color))
          .toList(),
    );
  }
}
