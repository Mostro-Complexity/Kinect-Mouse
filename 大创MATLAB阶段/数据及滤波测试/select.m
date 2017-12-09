function select(path)
src=load(path);
src=src(:,9:10);
figure; hold on

for i=1:6:size(src,1)
    i
    plot(src(1:i,1),src(1:i,2),'g');
    plot(kalman_filter(src(1:i,1),1e-6,4e-4,src(1,1),1),...
        kalman_filter(src(1:i,2),1e-6,4e-4,src(2,2),1),'b');
    legend('原数据','卡尔曼滤波后数据');
    pause(0.3);
end
end