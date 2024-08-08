import 'dart:io';

import 'package:flutter/material.dart';

import '../../widgets/pickers/user_image_picker.dart';

class AuthForm extends StatefulWidget {
  final void Function(
    String email,
    String password,
    String username,
    File? image,
    bool isLogin,
    BuildContext context,
  ) submitFn;

  final bool isLoading;

  AuthForm(this.submitFn, this.isLoading);

  @override
  State<AuthForm> createState() => _AuthFormState();
}

class _AuthFormState extends State<AuthForm> {
  final _formKey = GlobalKey<FormState>();
  bool _isLogin = true;

  String _userEmail = '';
  String _userName = '';
  String _userPassword = '';

  File? _userImageFile;

  void _pickedImage(File image) {
    _userImageFile = image;
  }

  void _trySubmit() {
    final isValid = _formKey.currentState?.validate();
    FocusScope.of(context).unfocus();

    if (_userImageFile == null && !_isLogin) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: const Text("Please pick an image."),
        backgroundColor: Theme.of(context).colorScheme.error,
      ));
      return;
    }

    if (isValid == null || isValid == false || _formKey.currentState == null) return;

    _formKey.currentState?.save();
    widget.submitFn(
      _userEmail.trim(),
      _userPassword.trim(),
      _userName.trim(),
      _userImageFile,
      _isLogin,
      context,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Card(
        margin: const EdgeInsets.all(20),
        child: SingleChildScrollView(
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Form(
              key: _formKey,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  if (!_isLogin) UserImagePicker(_pickedImage),
                  TextFormField(
                    key: const ValueKey('email'),
                    validator: (value) {
                      if (value == null) return 'Can not be empty';

                      if (value.isEmpty || !value.contains('@')) return 'Enter a valid email address';

                      return null;
                    },
                    keyboardType: TextInputType.emailAddress,
                    decoration: const InputDecoration(
                      labelText: 'Email address',
                    ),
                    onSaved: (value) {
                      if (value == null) return;

                      _userEmail = value;
                    },
                  ),
                  if (!_isLogin)
                    TextFormField(
                      key: const ValueKey('username'),
                      validator: (value) {
                        if (value == null) return 'Can not be empty';

                        if (value.isEmpty || value.length < 4) return 'Please enter at least 4 characters.';

                        return null;
                      },
                      decoration: const InputDecoration(labelText: 'Username'),
                      onSaved: (value) {
                        if (value == null) return;

                        _userName = value;
                      },
                    ),
                  TextFormField(
                    key: const ValueKey('password'),
                    validator: (value) {
                      if (value == null) return 'Can not be empty';

                      if (value.isEmpty || value.length < 7) return 'Password must be at least 7 characters long.';

                      return null;
                    },
                    decoration: const InputDecoration(labelText: 'Password'),
                    obscureText: true,
                    onSaved: (value) {
                      if (value == null) return;

                      _userPassword = value;
                    },
                  ),
                  const SizedBox(height: 12),
                  if (widget.isLoading) const CircularProgressIndicator(),
                  if (!widget.isLoading)
                    ElevatedButton(
                      onPressed: _trySubmit,
                      child: Text(_isLogin ? 'Login' : 'Register'),
                    ),
                  if (!widget.isLoading)
                    TextButton(
                      style: TextButton.styleFrom(
                        foregroundColor: Theme.of(context).buttonTheme.colorScheme?.primary,
                      ),
                      child: Text(_isLogin ? 'Create new account' : 'I already have an account'),
                      onPressed: () {
                        setState(() {
                          _isLogin = !_isLogin;
                        });
                      },
                    ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
