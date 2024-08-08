import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/product.dart';
import '../providers/products.dart';

class EditProductScreen extends StatefulWidget {
  static const identifier = '/edit-product';

  @override
  State<EditProductScreen> createState() => _EditProductScreenState();
}

class _EditProductScreenState extends State<EditProductScreen> {
  final _imageUrlController = TextEditingController();
  final _imageUrlFocusNode = FocusNode();
  final _form = GlobalKey<FormState>();

  var _edittedProduct = Product(
    id: '',
    title: '',
    description: '',
    price: 0,
    imageUrl: '',
  );

  var _initValues = {
    'title': '',
    'description': '',
    'price': '',
    'imageUrl': '',
  };
  var _isInit = true;
  var _isLoading = false;

  @override
  void initState() {
    _imageUrlController.addListener(_updateImageUrl);
    super.initState();
  }

  @override
  void didChangeDependencies() {
    if (_isInit) {
      String? productId = ModalRoute.of(context)?.settings.arguments as String?;

      if (productId != null) {
        _edittedProduct =
            Provider.of<Products>(context, listen: false).findById(productId);

        _initValues = {
          'title': _edittedProduct.title,
          'description': _edittedProduct.description,
          'price': _edittedProduct.price.toString(),
          'imageUrl': ''
        };

        _imageUrlController.text = _edittedProduct.imageUrl;
      }
    }

    _isInit = false;
    super.didChangeDependencies();
  }

  @override
  void dispose() {
    _imageUrlController.removeListener(_updateImageUrl);
    _imageUrlController.dispose();
    _imageUrlFocusNode.dispose();
    super.dispose();
  }

  void _updateImageUrl() {
    if (!_imageUrlFocusNode.hasFocus) {
      if (_imageUrlController.text.isEmpty ||
          (!_imageUrlController.text.startsWith('http') &&
              !_imageUrlController.text.startsWith('https'))) {
        return;
      }
      setState(() {});
    }
  }

  Future<void> _saveForm() async {
    final isValid = _form.currentState?.validate();
    if (isValid != null && !isValid) return;

    _form.currentState?.save();
    setState(() {
      _isLoading = true;
    });

    final productsProvider = Provider.of<Products>(context, listen: false);
    if (_edittedProduct.id.isNotEmpty) {
      await productsProvider.updateProduct(_edittedProduct.id, _edittedProduct);
    } else {
      try {
        await productsProvider.addProduct(_edittedProduct);
      } catch (e) {
        await showDialog(
          context: context,
          builder: (ctx) => AlertDialog(
            title: const Text('An error occured!'),
            content: const Text("Something went wrong!"),
            actions: [
              TextButton(
                child: const Text('Okay'),
                onPressed: () => Navigator.of(ctx).pop(),
              )
            ],
          ),
        );
      }
    }

    setState(() {
      _isLoading = false;
    });
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Edit Product'),
        actions: [
          IconButton(
            icon: const Icon(
              Icons.save,
              color: Colors.white,
            ),
            onPressed: _saveForm,
          )
        ],
      ),
      body: _isLoading
          ? const Center(
              child: CircularProgressIndicator(),
            )
          : Padding(
              padding: const EdgeInsets.all(16.0),
              child: Form(
                key: _form,
                child: ListView(
                  children: [
                    TextFormField(
                      decoration: const InputDecoration(labelText: 'Title'),
                      initialValue: _edittedProduct.title,
                      textInputAction: TextInputAction.next,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Enter a valid title';
                        }

                        return null;
                      },
                      onSaved: (value) {
                        _edittedProduct = Product(
                            id: _edittedProduct.id,
                            title: value ?? '',
                            description: _edittedProduct.description,
                            price: _edittedProduct.price,
                            imageUrl: _edittedProduct.imageUrl,
                            isFavorite: _edittedProduct.isFavorite);
                      },
                    ),
                    TextFormField(
                      decoration: const InputDecoration(labelText: 'Price'),
                      textInputAction: TextInputAction.next,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Please enter a price!';
                        }
                        if (double.tryParse(value) == null) {
                          return 'Enter a valid number';
                        }
                        if (double.parse(value) <= 0) {
                          return 'Please enter a number greather than zero';
                        }
                        return null;
                      },
                      initialValue: _edittedProduct.price.toString(),
                      keyboardType:
                          const TextInputType.numberWithOptions(decimal: true),
                      onSaved: (value) {
                        _edittedProduct = Product(
                            id: _edittedProduct.id,
                            title: _edittedProduct.title,
                            description: _edittedProduct.description,
                            price: value != null ? double.parse(value) : 0,
                            imageUrl: _edittedProduct.imageUrl,
                            isFavorite: _edittedProduct.isFavorite);
                      },
                    ),
                    TextFormField(
                      decoration:
                          const InputDecoration(labelText: 'Description'),
                      maxLines: 3,
                      keyboardType: TextInputType.multiline,
                      initialValue: _edittedProduct.description,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Please enter a description!';
                        }

                        if (value.length < 10) {
                          return 'Please enter a longer description';
                        }

                        return null;
                      },
                      onSaved: (value) {
                        _edittedProduct = Product(
                          id: _edittedProduct.id,
                          title: _edittedProduct.title,
                          description: value ?? '',
                          price: _edittedProduct.price,
                          imageUrl: _edittedProduct.imageUrl,
                          isFavorite: _edittedProduct.isFavorite,
                        );
                      },
                    ),
                    Row(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        Container(
                          width: 100,
                          height: 100,
                          margin: const EdgeInsets.only(top: 8, right: 10),
                          decoration: BoxDecoration(
                            border: Border.all(width: 1, color: Colors.grey),
                          ),
                          child: _imageUrlController.text.isEmpty
                              ? const Text('Enter a URL')
                              : FittedBox(
                                  child: Image.network(
                                    _imageUrlController.text,
                                    fit: BoxFit.cover,
                                  ),
                                ),
                        ),
                        Expanded(
                          child: TextFormField(
                            decoration:
                                const InputDecoration(labelText: 'Image URL'),
                            keyboardType: TextInputType.url,
                            textInputAction: TextInputAction.done,
                            controller: _imageUrlController,
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter an image url!';
                              }

                              if (!value.startsWith('http') &&
                                  !value.startsWith('https')) {
                                return 'Please enter a valid url.';
                              }

                              return null;
                            },
                            onSaved: (value) {
                              _edittedProduct = Product(
                                  id: _edittedProduct.id,
                                  title: _edittedProduct.title,
                                  description: _edittedProduct.description,
                                  price: _edittedProduct.price,
                                  imageUrl: value ?? '',
                                  isFavorite: _edittedProduct.isFavorite);
                            },
                            onFieldSubmitted: (_) {
                              _saveForm();
                            },
                          ),
                        ),
                      ],
                    )
                  ],
                ),
              ),
            ),
    );
  }
}
