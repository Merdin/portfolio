import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../../../models/transaction.dart';

class TransactionListItem extends StatelessWidget {
  final Transaction transaction;
  final Function handleDelete;

  TransactionListItem(this.transaction, this.handleDelete);

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 5,
      margin: EdgeInsets.symmetric(
        vertical: 8,
        horizontal: 5,
      ),
      child: ListTile(
        leading: CircleAvatar(
          radius: 30,
          child: Padding(
            padding: EdgeInsets.all(6),
            child: FittedBox(
              child: Text('\$${transaction.amount}'),
            ),
          ),
        ),
        title: Text(
          transaction.title,
          style: Theme.of(context).textTheme.titleMedium,
        ),
        subtitle: Text(
          DateFormat.yMMMd().format(transaction.date),
        ),
        trailing: MediaQuery.of(context).size.width > 460
            ? TextButton.icon(
                icon: Icon(
                  Icons.delete,
                  color: Theme.of(context).colorScheme.error,
                ),
                label: Text(
                  'Delete',
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.error,
                  ),
                ),
                onPressed: handleDelete,
              )
            : IconButton(
                icon: Icon(Icons.delete),
                color: Theme.of(context).colorScheme.error,
                onPressed: handleDelete,
              ),
      ),
    );
    ;
  }
}
