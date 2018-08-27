package androidhelper.brasshatstudios.androidhelper;

import android.content.ComponentName;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;

import java.io.ByteArrayOutputStream;
import java.util.List;

public class PackageInfo {

    public static boolean isSystem(ApplicationInfo applicationInfo){
        return (applicationInfo.flags & ApplicationInfo.FLAG_SYSTEM ) != 0;
    }

    public static List<ResolveInfo> installedApps(PackageManager pm) {
        final Intent main_intent = new Intent(Intent.ACTION_MAIN, null);
        main_intent.addCategory(Intent.CATEGORY_LAUNCHER);
        return pm.queryIntentActivities(main_intent, 0);
    }

    public static byte[] getIcon(PackageManager pm, ResolveInfo info) {
        try {

            Drawable icon = info.loadIcon(pm);
            Bitmap bmp = Bitmap.createBitmap(icon.getIntrinsicWidth(), icon.getIntrinsicHeight(), Bitmap.Config.ARGB_8888);
            Canvas can = new Canvas(bmp);
            icon.setBounds(0, 0, can.getWidth(), can.getHeight());
            icon.draw(can);
            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            bmp.compress(Bitmap.CompressFormat.PNG, 100, stream);
            byte[] byteArray = stream.toByteArray();
            return byteArray;
        } catch (Exception e) {
            return null;
        }
    }

    public static Intent getIntent(ActivityInfo activity)
    {
        ComponentName name = new ComponentName(activity.applicationInfo.packageName,
                activity.name);
        Intent intent = new Intent(Intent.ACTION_MAIN);

        intent.addCategory(Intent.CATEGORY_LAUNCHER);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK |
                Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED);
        intent.setComponent(name);

        return intent;
    }
}