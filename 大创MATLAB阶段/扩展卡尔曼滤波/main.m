cross=load('cross.txt');
cross=cross(:,9:10);
adj=[extended_kalman_filter(cross(:,1),0.04,0.4) ...
    extended_kalman_filter(cross(:,2),0.04,0.4)];
figure
plot(cross(:,1),cross(:,2));
hold on
plot(adj(:,1),adj(:,2));
legend('原数据','扩展卡尔曼滤波');