import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import './providers/auth.dart';
import './providers/orders.dart';
import './providers/products.dart';
import './providers/cart.dart';

import './screens/splash_screen.dart';
import './screens/products_overview_screen.dart';
import './screens/edit_product_screen.dart';
import './screens/cart_screen.dart';
import './screens/product_detail_screen.dart';
import './screens/orders_screen.dart';
import './screens/user_products_screen.dart';
import './screens/auth_screen.dart';

import 'helpers/custom_route.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => Auth()),
        ChangeNotifierProxyProvider<Auth, Products>(
          create: (ctx) => Products('', '', []),
          update: (ctx, auth, previousProducts) =>
              Products(auth.token ?? '', auth.userId ?? '', previousProducts == null ? [] : previousProducts.items),
        ),
        ChangeNotifierProvider(create: (_) => Cart()),
        ChangeNotifierProxyProvider<Auth, Orders>(
          create: (ctx) => Orders('', '', []),
          update: (ctx, auth, previousOrders) =>
              Orders(auth.token ?? '', auth.userId ?? '', previousOrders == null ? [] : previousOrders.orders),
        ),
      ],
      child: const MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<Auth>(
      builder: (ctx, auth, _) => MaterialApp(
        title: 'MyShop',
        theme: ThemeData(
          colorScheme: ColorScheme.fromSwatch(
            primarySwatch: Colors.lightGreen,
            accentColor: Colors.orange,
          ),
          fontFamily: 'Lato',
          pageTransitionsTheme: PageTransitionsTheme(builders: {
            TargetPlatform.iOS: CustomPageTransitionBuilder(),
            TargetPlatform.android: CustomPageTransitionBuilder(),
          }),
        ),
        home: auth.isAuth
            ? ProductsOverviewScreen()
            : FutureBuilder(
                future: auth.tryAutoLogin(),
                builder: (ctx, snapshot) =>
                    snapshot.connectionState == ConnectionState.waiting ? SplashScreen() : AuthScreen(),
              ),
        routes: {
          AuthScreen.identifier: (context) => AuthScreen(),
          ProductsOverviewScreen.identifier: (context) => ProductsOverviewScreen(),
          ProductDetailScreen.identifier: (context) => ProductDetailScreen(),
          CartScreen.identifier: (context) => CartScreen(),
          OrdersScreen.identifier: (context) => OrdersScreen(),
          UserProductsScreen.identifier: (context) => UserProductsScreen(),
          EditProductScreen.identifier: (context) => EditProductScreen(),
        },
      ),
    );
  }
}
