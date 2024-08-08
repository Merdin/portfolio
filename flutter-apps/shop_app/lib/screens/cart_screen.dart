import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/cart.dart';
import '../widgets/cart_item.dart' as ci;
import '../providers/orders.dart';

class CartScreen extends StatelessWidget {
  static const identifier = '/cart';

  @override
  Widget build(BuildContext context) {
    final cart = context.watch<Cart>();

    return Scaffold(
      appBar: AppBar(
        title: const Text('Your Cart'),
      ),
      body: Column(children: [
        Card(
          margin: const EdgeInsets.all(15),
          child: Padding(
            padding: const EdgeInsets.all(8),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  'Total',
                  style: TextStyle(fontSize: 20),
                ),
                const Spacer(),
                Chip(
                  label: Text(
                    '\$${cart.totalAmount.toStringAsFixed(2)}',
                    style: TextStyle(
                      color:
                          Theme.of(context).primaryTextTheme.titleMedium?.color,
                    ),
                  ),
                  backgroundColor: Theme.of(context).primaryColor,
                ),
                OrderButton(cart: cart),
              ],
            ),
          ),
        ),
        Expanded(
          child: ListView.builder(
            itemBuilder: (ctx, index) {
              final cartItem = cart.items.values.toList()[index];

              return ci.CartItem(
                cartItem.id,
                cart.items.keys.toList()[index],
                cartItem.price,
                cartItem.quantity,
                cartItem.title,
              );
            },
            itemCount: cart.itemCount,
          ),
        )
      ]),
    );
  }
}

class OrderButton extends StatefulWidget {
  const OrderButton({
    super.key,
    required this.cart,
  });

  final Cart cart;

  @override
  State<OrderButton> createState() => _OrderButtonState();
}

class _OrderButtonState extends State<OrderButton> {
  var _isLoading = false;

  @override
  Widget build(BuildContext context) {
    return TextButton(
      onPressed: (widget.cart.totalAmount <= 0 || _isLoading)
          ? null
          : () async {
              setState(() {
                _isLoading = true;
              });
              await context.read<Orders>().addOrder(
                    widget.cart.items.values.toList(),
                    widget.cart.totalAmount,
                  );
              widget.cart.clear();
              setState(() {
                _isLoading = false;
              });
            },
      child: _isLoading
          ? const CircularProgressIndicator()
          : const Text('ORDER NOW'),
    );
  }
}
