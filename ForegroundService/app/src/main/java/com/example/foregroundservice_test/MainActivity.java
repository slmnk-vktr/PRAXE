package com.example.foregroundservice_test;



import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;
import androidx.navigation.fragment.NavHostFragment;

import android.Manifest;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

public class MainActivity extends AppCompatActivity {
        private EditText editTextInput;

        @Override
        protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);

            editTextInput = findViewById(R.id.edit_text_input);

        }

        //IP serveru
        String IP = "192.168.2.209";


        public void startService(View v) {


            String input = editTextInput.getText().toString();


            //checks if SMS permissions were given

            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
                if (checkSelfPermission(Manifest.permission.SEND_SMS) == PackageManager.PERMISSION_GRANTED) {


                    Intent serviceIntent = new Intent(this, ForegroundService.class);
                    serviceIntent.putExtra("inputExtra", input);
                    ContextCompat.startForegroundService(this, serviceIntent);

                }
                else
                {
                    requestPermissions(new String[]{Manifest.permission.SEND_SMS}, 1);
                }
            }



        }
        public void status(View v) {


            String input = editTextInput.getText().toString();
            if (input.length() > 5) {
                Log.e("IP:", input);
                IP = input;
            }

            StringRequest sr = new StringRequest("http://"+IP+":3000/api/sms", new Response.Listener<String>() {

                @Override
                public void onResponse(String response) {
                    Toast.makeText(MainActivity.this, "Connected to the server", Toast.LENGTH_SHORT).show();
                }
            }, new Response.ErrorListener() {

                @Override
                public void onErrorResponse(VolleyError error) {
                    Toast.makeText(MainActivity.this, "Error while trying to connect to the server", Toast.LENGTH_SHORT).show();
                }
            });
            RequestQueue xx = Volley.newRequestQueue(MainActivity.this);
            xx.add(sr);

        }

    public void register(View v) {

        String input = editTextInput.getText().toString();
        if (input.length() > 5) {
            Log.e("IP:", input);
            IP = input;
        }

        String deviceInfo = System.getProperty("os.version")+"_____"+  android.os.Build.DEVICE +"_____"+  android.os.Build.MODEL+"_____"+ android.os.Build.ID;
        Log.e(deviceInfo, deviceInfo);

        StringRequest sr = new StringRequest(Request.Method.POST,"http://"+IP+":3000/api/user/"+deviceInfo, new Response.Listener<String>() {

            @Override
            public void onResponse(String response) {


                Toast.makeText(MainActivity.this, response, Toast.LENGTH_SHORT).show();
            }
        }, new Response.ErrorListener() {

            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(MainActivity.this, "Error: "+error, Toast.LENGTH_SHORT).show();
            }
        });
        RequestQueue xx = Volley.newRequestQueue(MainActivity.this);
        xx.add(sr);

    }

    public void logout (View v)
    {
        String input = editTextInput.getText().toString();
        if (input.length() > 5) {
            Log.e("IP:", input);
            IP = input;
        }

        String deviceInfo = System.getProperty("os.version")+"_____"+  android.os.Build.DEVICE +"_____"+  android.os.Build.MODEL+"_____"+ android.os.Build.ID;
        Log.e(deviceInfo, deviceInfo);

        StringRequest sr = new StringRequest(Request.Method.DELETE,"http://"+IP+":3000/api/user/"+deviceInfo, new Response.Listener<String>() {

            @Override
            public void onResponse(String response) {


                Toast.makeText(MainActivity.this, response, Toast.LENGTH_SHORT).show();
            }
        }, new Response.ErrorListener() {

            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(MainActivity.this, "Error: "+error, Toast.LENGTH_SHORT).show();
            }
        });
        RequestQueue xx = Volley.newRequestQueue(MainActivity.this);
        xx.add(sr);

    }


}



