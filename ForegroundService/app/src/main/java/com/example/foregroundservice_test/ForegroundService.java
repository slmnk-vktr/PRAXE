package com.example.foregroundservice_test;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import android.app.Activity;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.Build;
import android.os.Bundle;
import android.os.IBinder;
import android.telephony.SmsManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import android.net.Uri;

import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;
import androidx.core.app.NotificationManagerCompat;

import static com.example.foregroundservice_test.App.CHANNEL_ID;



public class ForegroundService extends Service {



    private Context mContext;

    String IP = "192.168.2.209";
    String id;
    String fullMsg;     //response
    String msg;         //sms txt
    String phone;       //phone number
    String status;





    @Override
    public void onCreate() {
        super.onCreate();

    }


    MediaPlayer player;
    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {




        mContext = this;


        String input = intent.getStringExtra("inputExtra");
        if (input.length() > 5)
        {
            Log.e("IP:", input);
            IP = input;
        }


        Intent notificationIntent = new Intent(this, MainActivity.class);
        PendingIntent pendingIntent = PendingIntent.getActivity(this,
                0, notificationIntent, 0);



        Notification notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("SMS Gateway")
//                .setContentText(status)
                .setSmallIcon(R.drawable.ic_android_foreground_service)
                .setContentIntent(pendingIntent)
                .build();




        new Thread(new Runnable(){
            public void run() {

                while(true)
                {
                    try {
                        Thread.sleep(2000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }



                    StringRequest sr = new StringRequest("http://"+IP+":3000/api/sms", new Response.Listener<String>() {

                        @Override
                        public void onResponse(String response) {
                            if (response.equals("0")) {
                                //here comes the specific response
                                Log.e("No SMS to be sent",response.toString());
                                status = "Connected";



                            } else {

                                id = response.toString();
                                Log.e("http://" + IP + ":3000/api/sms/"+id,response.toString());
                                status = "Connected";


                                //Asks for msg with given ID
                                StringRequest sr2 = new StringRequest(Request.Method.GET,"http://" + IP + ":3000/api/sms/"+id, new Response.Listener<String>() {
                                    @Override
                                    public void onResponse(String response) {
                                        fullMsg = response.toString();

                                        phone = fullMsg.substring(fullMsg.length()-9);
                                        msg = fullMsg.substring(0,fullMsg.length()-10);

                                        //Notification
                                        Toast.makeText(ForegroundService.this, "SMS has been sent", Toast.LENGTH_SHORT).show();


                                        //SEND SMS and DELETE IT + notification of successfully sending sms




                                            try {

                                                SmsManager smsManager = SmsManager.getDefault();
                                                smsManager.sendTextMessage(phone, null,msg,null,null);



                                                Log.e("SMS has been sent", phone);



                                                StringRequest sr3 = new StringRequest(Request.Method.POST,"http://" +IP+":3000/api/sms/"+id, new Response.Listener<String>() {
                                                    @Override
                                                    public void onResponse(String response) {
                                                        Log.e("SMS marked as sent", response.toString());


                                                    }

                                                }, new Response.ErrorListener() {
                                                    @Override
                                                    public void onErrorResponse(VolleyError error) {
                                                        Log.e("ERORR!!!!!!!!",error.toString());
                                                        Log.e("IP:", input);
                                                    }
                                                });
                                                RequestQueue xx = Volley.newRequestQueue(ForegroundService.this);
                                                xx.add(sr3);


                                                  Thread.sleep(100);
//                                                mContext.getContentResolver().delete(Uri.parse("content://sms/"), null, null);
                                            }
                                            catch (Exception e)
                                            {

                                                Log.e("Failed to sent message to number", phone);
                                            }


                                        Log.e(id, String.valueOf(phone));
                                        Log.e(id, msg);
                                    }
                                }, new Response.ErrorListener() {
                                    @Override
                                    public void onErrorResponse(VolleyError error) {


                                        //Notification
                                        Toast.makeText(ForegroundService.this, "Failed to sent message", Toast.LENGTH_SHORT).show();
                                        Log.e("ERORR!!!!!!!!",error.toString());

                                    }
                                });
                                RequestQueue xx = Volley.newRequestQueue(ForegroundService.this);
                                xx.add(sr2);

                            }
                        }
                    }, new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {

                            status = "Not connected";
                            Log.e("ERORR!!!!!!!!",error.toString());
                            Log.e("IP:", input);
                        }
                    });
                    RequestQueue xx = Volley.newRequestQueue(ForegroundService.this);
                    xx.add(sr);





                }


            }
        }).start();


        startForeground(1, notification);
        return START_NOT_STICKY;
    }



    @Override
    public void onDestroy() {
        super.onDestroy();
    }


    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}