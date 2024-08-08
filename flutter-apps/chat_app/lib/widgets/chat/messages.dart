import 'package:flutter/material.dart';

import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_auth/firebase_auth.dart';

import '../../widgets/chat/message_bubble.dart';

class Messages extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: FirebaseFirestore.instance.collection('chat').orderBy('created_at', descending: true).snapshots(),
      builder: (ctx, chatSnapshot) {
        if (chatSnapshot.connectionState == ConnectionState.waiting) {
          return const Center(child: CircularProgressIndicator());
        }

        final chatDocs = chatSnapshot.data?.docs;
        final user = FirebaseAuth.instance.currentUser;

        if (chatDocs == null || user == null) {
          return const CircularProgressIndicator();
        } else {
          return ListView.builder(
            reverse: true,
            itemCount: chatDocs.length,
            itemBuilder: (ctx, index) => MessageBubble(
              chatDocs[index]['text'],
              chatDocs[index]['username'],
              chatDocs[index]['user_image'],
              chatDocs[index]['user_id'] == user.uid,
              key: ValueKey(chatDocs[index].id),
            ),
          );
        }
      },
    );
  }
}
