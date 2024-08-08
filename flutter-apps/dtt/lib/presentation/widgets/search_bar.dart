import 'package:flutter/material.dart';

class SearchBar extends StatelessWidget {
  final TextEditingController _searchController;
  final FocusNode _searchFocusNode;
  final void Function(String text) onChanged;
  final void Function() onIconPressed;

  SearchBar(
    this._searchController,
    this._searchFocusNode, {
    required this.onIconPressed,
    required this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return TextField(
      focusNode: _searchFocusNode,
      controller: _searchController,
      decoration: InputDecoration(
        contentPadding: const EdgeInsets.only(left: 16.0),
        filled: true,
        alignLabelWithHint: true,
        hintText: 'Search for a home',
        suffixIcon: IconButton(
          icon: _searchController.text.isEmpty
              ? const Icon(Icons.search, color: Colors.grey)
              : const Icon(Icons.clear, color: Colors.black),
          onPressed: _searchController.text.isEmpty ? null : onIconPressed,
        ),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8.0),
          borderSide: BorderSide.none,
        ),
      ),
      onChanged: onChanged,
    );
  }
}
