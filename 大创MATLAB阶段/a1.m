pts=load('hand_and_thumb.txt'); hands=pts(:,9:10);
hand1=hands(90:118,:);  % ×ó->ÓÒ
hand2=hands(118:145,:); % ÓÒ->×ó
figure; hold on
axis([-0.4, 0.4, 0, 0.5 ]);
newhand1=hand1(1:2,:);
for i=3:size(hand1,1)
    i
    dx= (hand1(i,1)-hand1(1,1)) /(i-1)
    ddx
    x=newhand1(end,1)+dx;
    y=mean(hand1(1:i,2));
    newhand1(end+1,: )=[x y];
    plot(hand1(1:i,1),hand1(1:i,2),'k*-');
    plot(newhand1(1:i,1),newhand1(1:i,2),'b*-');
    pause
end
