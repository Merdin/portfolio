import 'package:flutter/material.dart';
// ignore: depend_on_referenced_packages
import 'package:provider/provider.dart';

import '../providers/products.dart';
import './product_item.dart';

class ProductsGrid extends StatelessWidget {
  final bool _showFavorites;

  ProductsGrid(this._showFavorites);

  @override
  Widget build(BuildContext context) {
    final products = context.watch<Products>();
    final filteredItems =
        _showFavorites ? products.favoriteItems : products.items;

    return GridView.builder(
      padding: const EdgeInsets.all(10),
      itemCount: filteredItems.length,
      itemBuilder: (context, index) {
        return ChangeNotifierProvider.value(
          value: filteredItems[index],
          child: ProductItem(),
        );
      },
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        childAspectRatio: 1.5,
        crossAxisSpacing: 10,
        mainAxisSpacing: 10,
      ),
    );
  }
}
