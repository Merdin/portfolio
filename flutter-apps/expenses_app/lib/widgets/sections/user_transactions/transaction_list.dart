import 'package:flutter/material.dart';

import '../../../models/transaction.dart';
import './transaction_list_item.dart';

class TransactionsList extends StatelessWidget {
  final List<Transaction> transactions;
  final Function handleDelete;

  TransactionsList(this.transactions, this.handleDelete);

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(builder: (ctx, constraints) {
      return transactions.isEmpty
          ? Column(
              children: [
                Text(
                  'No transactions added yet!',
                  style: Theme.of(context).textTheme.titleMedium,
                ),
                SizedBox(height: 10),
                Container(
                  height: constraints.maxHeight * 0.6,
                  child: Image.asset(
                    'assets/images/waiting.png',
                    fit: BoxFit.cover,
                  ),
                ),
              ],
            )
          : ListView.builder(
              itemBuilder: (context, index) {
                return TransactionListItem(
                  transactions[index],
                  () => handleDelete(index),
                );
              },
              itemCount: transactions.length,
            );
    });
  }
}
