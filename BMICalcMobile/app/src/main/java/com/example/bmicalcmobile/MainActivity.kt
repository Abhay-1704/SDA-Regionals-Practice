package com.example.bmicalcmobile

import android.graphics.Color
import android.os.Bundle
import android.text.SpannableString
import android.text.Spannable
import android.text.style.ForegroundColorSpan
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val etWeight     = findViewById<EditText>(R.id.editTextText2)
        val etHeight     = findViewById<EditText>(R.id.editTextText)
        val btnCalculate = findViewById<Button>(R.id.button)
        val tvResult     = findViewById<TextView>(R.id.textView)

        btnCalculate.setOnClickListener {

            if (etWeight.text.isNullOrBlank() || etHeight.text.isNullOrBlank()) {
                tvResult.text = "Please fill in all fields!"
                return@setOnClickListener
            }

            val weight     = etWeight.text.toString().toDouble()
            val heightCm   = etHeight.text.toString().toDouble()
            val heightM    = heightCm / 100
            val bmi        = weight / (heightM * heightM)
            val bmiRounded = String.format("%.2f", bmi)

            val (category, color) = when {
                bmi < 18.5 -> Pair("Underweight",     Color.BLUE)
                bmi < 25.0 -> Pair("Normal Weight ✓", Color.parseColor("#4CAF50"))
                bmi < 30.0 -> Pair("Overweight",      Color.parseColor("#FF9800"))
                else       -> Pair("Obese",            Color.RED)
            }

            // Only color the NEW line, not everything
            val newLine = SpannableString("\nBMI: $bmiRounded — $category")
            newLine.setSpan(ForegroundColorSpan(color), 0, newLine.length, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)

            // Append colored line to existing text
            tvResult.append(newLine)

            etWeight.text.clear()
            etHeight.text.clear()
        }
    }
}