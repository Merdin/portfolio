import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

class AboutScreen extends StatelessWidget {
  Future<void> _launchUrl() async {
    final url = Uri.parse("https://www.d-tt.nl/over-dtt");

    if (!await launchUrl(url)) {
      throw Exception('Could not launch $url');
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.all(20.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              "ABOUT",
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 30),
            const Text(
              "Aute non est laborum magna laborum tempor ut sint reprehenderit duis deserunt exercitation culpa. Non esse labore officia Lorem anim qui ut ut ea tempor. Tempor commodo id amet amet sint. Officia labore amet sint nisi ut dolor amet laborum deserunt Lorem irure est eiusmod. Incididunt irure ullamco Lorem labore amet fugiat. Do proident occaecat proident voluptate adipisicing incididunt dolor reprehenderit nisi aliqua enim non laborum duis.",
            ),
            const SizedBox(height: 30),
            const Text(
              "Design and Development",
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 20),
            Wrap(
              spacing: 30,
              crossAxisAlignment: WrapCrossAlignment.center,
              children: [
                const Image(
                  image: AssetImage('assets/images/dtt_logo.png'),
                  width: 140,
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text("by DTT"),
                    InkWell(
                      onTap: _launchUrl,
                      child: const Text(
                        "d-tt.nl",
                        style: TextStyle(color: Colors.blue),
                      ),
                    ),
                  ],
                )
              ],
            ),
          ],
        ),
      ),
    );
  }
}
